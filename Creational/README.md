# Creational Patterns

Deals with the creation of objects. Creational design patterns provide various object creation mechanisms, which increase flexibility and reuse of existing code.

## Builder

Construct complex objects step by step. The pattern allows you to produce different types and representations of an object using the same construction code.

## Factories

### Factory Method
Provides an interface for creating objects in a superclass, but allows subclasses to alter the type of objects that will be created.

### Abstract Method

Produce families of related objects without specifying their concrete classes.

## Prototype

Provides a mechanism to copy the original object to a new object and then modify it according to our needs.

## Singleton

Ensure that a class has only one instance, while providing a global access point to this instance.

## Disadvantages

The use of singleton can make it harder to test the code as its difficult to mock the behaviour. 

Always use interfaces to implement your singleton, together with dependency injection to work around this iussue.

Remember not to create dependencies on concrete classes, rather abstraction (interfaces).


