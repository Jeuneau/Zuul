using System;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;
	private Inventory inventory;
	


	// Private fields
	

	// Constructor
	public Game()
	{
		parser = new Parser();
		inventory = new Inventory(100); // Assuming 100 is the max weight
		player = new Player();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");
		

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);

		theatre.AddExit("west", outside);

		pub.AddExit("east", outside);
		

		lab.AddExit("north", outside);
		lab.AddExit("east", office);
		lab.AddExit("down", theatre);

		office.AddExit("west", lab);
		office.AddExit("up", pub);

		// Create your Items here
		// ...
		// And add them to the Rooms
		// ...

		// Start game outside
		player.currentRoom = outside;
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished && player.health > 0)
		{
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("You died. Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	
		if (player.currentRoom.Description == "outside the main entrance of the university" && player.backpack.Get("key") != null)
		{
			Win();
		}
	}

		
	//Win scenario
	public void Win() {
		Console.WriteLine("You have unlocked the door and escaped the university.");
		Console.WriteLine("You have won the game.");
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
				Put(command); 
				break;
			case "get":
				Get(command); 
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
			Console.WriteLine("There is no door to "+direction+"!");
			return;
		}

		player.currentRoom = nextRoom;
		Console.WriteLine(player.currentRoom.GetLongDescription());
		player.health -= 10;
	}

	private void Look(Command command)
	{
		Console.WriteLine(player.currentRoom.GetLongDescription());
	}

	private void Status(Command command)
	{
		Console.WriteLine("Your health is: " + player.health);
		Console.WriteLine("Your inventory: " + inventory.Show());
	}

	private void Put(Command command)
	{
		if (command.SecondWord == null)
		{
			Console.WriteLine("Put what?");
			return;
		}

		string itemName = command.SecondWord; // Assuming the item name is the second word in the command
		Item backpackItem = player.backpack.Get(itemName);

		if (backpackItem != null)
		{
			inventory.Put(itemName, backpackItem);
			Console.WriteLine("You have put " + backpackItem.Description + " in the chest.");
		}
		else
		{
			Console.WriteLine("Item not found in your inventory.");
		}
	}

	private void Get(Command command)
	{
		if (command.SecondWord == null)
		{
			Console.WriteLine("Get what?");
			return;
		}

		string itemName = command.SecondWord; // Assuming the item name is the second word in the command
		Item chestItem = player.currentRoom.Chest.Get(itemName);

		if (chestItem != null)
		{
			inventory.Get(itemName);
			Console.WriteLine("You have fetched " + chestItem.Description + " from the chest.");
		}
		else
		{
			Console.WriteLine("Item not found in the chest.");
		}
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

	private void Use(Command command)
	{
		if (command.SecondWord == null)
		{
			Console.WriteLine("Use what?");
		}
		else if (command.SecondWord == "key" && command.ThirdWord == "west")
		{
			Console.WriteLine("You have unlocked the gate and escaped the university. Congratulations!");
		}
		if(command.SecondWord == "potion")
		{
			player.Heal(20); 
			Console.WriteLine("You have healed yourself.");
		}
	}

		

	private void Attack(Command command)
	{
		if (command.SecondWord == null)
		{
			Console.WriteLine("Attack what?");
		}
		if (command.SecondWord == "dragon" && command.ThirdWord == "sword")
		{
			Console.WriteLine("You have sliced the dragon in half.");
		}
		else if (command.SecondWord == "dragon" && command.ThirdWord == "shotgun")
		{
			Console.WriteLine("You have punctured the dragon's scales.");
		}
		else if(command.SecondWord == "dragon" && command.ThirdWord == "grenade")
		{
			Console.WriteLine("You have blown the dragon to pieces.");
		}
	}
}
