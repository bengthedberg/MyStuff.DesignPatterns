# Builder 

[refactoring.guru](https://refactoring.guru/design-patterns/builder)

## Motivation

* Some objects are not simple and can be created in single constructor call.
* Other objects require alot of cermony to create.
* Having a class with lots of constructor arguments is not productive and can cause confusion in how to use the class.

> Simplify the creation of objects by providing a piecewise construction.    

> Provide an API for construction an object step-by-step.


## Advantage

A builder is a component that is solely responsible for object creation. Introducing builder components clearly separates your logic and model classes from creating and initializing the desired instance.

The main benefit of this creational pattern is that it makes the codebase more readable and maintainable.

## Limitations

Despite the advantages it brings with it, the fluent builder makes it quite hard to use with inheritance. 
You would need to use Recursive Generics. This approach works, however, makes the builder code more complex.

> Extension method on the builder is another way to add additional methods in combination with a functional builder.