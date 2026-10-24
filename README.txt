Object Pool: 
	Created a singleton class with a list of game objects that are stored within. Two public functions are available for other classes to use, Destroy(GameObject) and Create(GameObject, Vector3). Destroy sets an object to be inactive and adds reference to it in the list contained within the Object Pool class. The Create function takes a game object and checks the Object Pool's list to see if it is contained within. If the item is found within the object pool's list it has its transform set to the specified Vector3 point, and set active while removing it from the Object Pool's list of game objects. If the object is not found within the Object Pool, it is simply Instantiated in, for later use when that object is destroyed.
	When one of the player's bullets is spawned by the PlayerShoot class, it is run through the Object Pool's Create function as opposed to being instantiated. When a bullet is destroyed upon reaching the edge of the screen it is destroyed through the Object Pool's Destroy function.


Builder Pattern:
	A builder pattern was used to create the different types of targets for the carnival shooting game. With an interface defining the different functions the TargetBuilder class can use to alter the properties of a target while building the target. The TargetBuilder class attached to the "Managers" game object serves as the backend for construction. Containing functions for building the various target components, as well as where those targets should be spawned and the base target prefab. The TargetDirector class on the same object then handles choosing what targets should be constructed and at what time. The director will pick a target to spawn and then, using an array of TargetClasses (which is a class storing the information that then needs to be passed to a target) constructs the target according to the specifications provided using the builder function.

	Currently the code only has two different types of targets, a standard target and a fast target worth more points, but a class containing target info allows for easy expansion with adjustments made to the spawning parameters. 


Observer Pattern:
	An observer is attached to each target object, which notifies all observers in its observer list upon being hit. Currently the two listening functions is a function for adding an amount to the score (Determined by the TargetBase class), and a function for updating the score UI. As is the case with observers more can be attached to that event if desired.
	The IObserver interface sets up the observering functions with the ListenTrigger function being contained within for triggering the events tied to that function, and the IScore interface sets up the observed object containing setup for functions for starting to observe the object's Notify function (StartListening), a func for untethering from the Notify function (StopListening), the Notify function which triggers the ListenTrigger function for all functions who are observers of the observed object.
	Currently the ScoreTrigger script functions as the Observed class and the TargetBase class and the ScoreManager class serve as the Observer classes.

-Written by Jacob Geniesse