# SmartLogic Circit Generator
----

This is a 3rd party project that is designed to help player of the game "smart logic simulator"
 to create advanced circuits using a programatic way.
<br> 
The save files in the game are in json format and are human readable.
<br>
The project enables the developers to create components and connection between them
and generates save file out of them.

----
## Classes
 - _Component_ - This is the base class for all basic game components, such as logic gates and flip flops.
 - _ComponentFactory_ - This is a static class with methods that create most in game components.
 - _IC_ - This is a class that takes a list of components and 
        generates a save file out of them and the connections they include. 
 - _CompositeComponent_ - This is the base class for all special components that integrate many in game components together.
     <br>
      for example andGate, Adder, Multiplyer, Register, etc...
     <br>
      It has an abstract method called getChildren, which return all Components inside the CompositeComponents.

----
## CompositeComponents
 - _UnsignedAdder_ - Has two numBits size inputs and numBits+1 size output which is the addition of the numbers.
 - _Register_ - Has n bit input together with a write 1 bit input, saves the value of the current input when write changes from 0 to 1.
 - _Multiplyer_ - Has two inputs: n bit and m bit and an n+m bit output which is their multiplication.