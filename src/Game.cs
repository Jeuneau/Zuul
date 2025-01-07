using System;
using System.ComponentModel;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;
    private Room outside;
	private Room theatre;
	private Room pub;
	private Room lab;
	private Room office;
	private Room basement;
	private bool finished;
	








   


    // Constructor
    public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
		
	}

	
	

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		outside = new Room("outside the main entrance of the university. A dragon is burning the campus down. Tread carefully");
		theatre = new Room("in a lecture theatre. You see a shotgun laying on the floor");
		pub = new Room("in the campus pub. You see a sword on the wall, a Zweihander to be exact");
		lab = new Room("in a computing lab. You see a potion on the desk");
		office = new Room("in the computing admin office. You find a grenade in the drawer of the desk");
		basement = new Room("in the basement of the university. You see a key on the floor and a staircase leading up to the theatre");

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);

		theatre.AddExit("west", outside);
		theatre.AddExit("down", basement);

		pub.AddExit("east", outside);
		

		lab.AddExit("north", outside);
		lab.AddExit("east", office);
		lab.AddExit("down", theatre);

		office.AddExit("west", lab);
		office.AddExit("up", pub);

		basement.AddExit("up", theatre);

		// Create and assign items and weapons to rooms
		theatre.Chest.Put("shotgun", new Item(10, "a shotgun"));
		pub.Chest.Put("sword", new Item(10, "a Zweihander"));
		lab.Chest.Put("potion", new Item(5, "a potion"));
		office.Chest.Put("grenade", new Item(5, "a grenade"));
		basement.Chest.Put("key", new Item(1, "a key"));
		

		// Start game outside
		player.currentRoom = outside;
	}

	

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		// Game over
		finished = false;
		while (!finished && player.Health > 0 && player.HasKey == false)
		{
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("You died. Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.currentRoom.GetLongDescription());
	}

	private void Win ()
	{
		Console.WriteLine("You have escaped the university. Congratulations!");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "look":
				Look(command);
				break;
			case "status":
				Status(command);
				break;
			case "put":
				player.Put(command); 
				break;
			case "get":
				player.Get(command); 
				break;
			case "take":
				Take(command); 
				break;
			case "drop":
				Drop(command); 
				break;
			case "use":
				Use(command); 
				break;
			case "attack":
				Attack(command);
				break;
		}

		return wantToQuit;
	}

	

	

	// ######################################
	// implementations of user commands:
	// ######################################
	
	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.currentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+ direction +"!");
			return;
		}

		player.currentRoom = nextRoom;
		Console.WriteLine(player.currentRoom.GetLongDescription());
		player.Health -= 10;
	}

	private void Look(Command command)
	{
		Console.WriteLine(player.currentRoom.GetLongDescription());
	}

	private void Status(Command command)
	{
		Console.WriteLine("Your health is: " + player.Health);
		Console.WriteLine("Your inventory: " + player.Backpack.Show());
	}



	private void Take(Command command)
	{	
		if (command.SecondWord == null)
		{
			Console.WriteLine("Take what?");
			return;
		}

		string itemName = command.SecondWord; // Assuming the item name is the second word in the command
		player.TakeFromChest(itemName);
	}

	private void Drop(Command command)
	{
		if (command.SecondWord == null)
		{
			Console.WriteLine("Drop what?");
			return;
		}
		string itemName = command.SecondWord; // Assuming the item name is the second word in the command
		player.DropToChest(itemName);
	}

	private void Use(Command command) // to do: implement that the player cannot enter input if the key is used to open the gate
	{
		if (command.SecondWord == null)
		{
			Console.WriteLine("Use what?");
		}
		else if (command.SecondWord == "key" && command.ThirdWord == "west" && player.currentRoom == outside && player.Backpack.HasItem("key"))
		{
			player.Backpack.RemoveItem("key");
			finished = true;
			Win();
		}
		if(command.SecondWord == "potion" && player.Backpack.HasItem("potion"))
		{
			player.Backpack.RemoveItem("potion");
			player.Heal(20); 
			Console.WriteLine("You have healed yourself.");
		}
		else
		{
			Console.WriteLine("You don't have or can't use that item.");
		}
	}

		

	private void Attack(Command command) 
	{
		if (command.SecondWord == null)
		{
			Console.WriteLine("Attack what?");
		}
		else if (player.currentRoom == outside)
		{
			if (command.SecondWord == "dragon" && command.ThirdWord == "sword")
			{
				Console.WriteLine("You have sliced the dragon in half.");
			}
			else if (command.SecondWord == "dragon" && command.ThirdWord == "shotgun")
			{
				Console.WriteLine("You have punctured the dragon's scales.");
			}
			else if (command.SecondWord == "dragon" && command.ThirdWord == "grenade")
			{
				Console.WriteLine("You have blown the dragon to pieces.");
			}
		}
		else
		{
			Console.WriteLine("There is nothing to attack here.");
		}
	}
}


