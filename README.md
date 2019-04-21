# DIComponents

DIComponents is a dependency injection framework created for unit 3d. It is used to check and inject component dependencies in unity scripts and does not contain containers for dependency injection. It is simply a substitute for the functionality of unity 3D, such as the GetComponent, transform.find, etc.

# Usage
1. Add an empty game object to the scene and add the MonoDIComponentsInitializer script. This script is used to inject dependencies for objects on the scene.

2. In unity scripts inherited from monoBenavior, add attributes to the fields for caching components.
```C#
public class ExampleClass : MonoBehaviour
{
  [InjectComponent] private Weapon weapon;
  
  ...
}
```

Currently, the framework supports the following attributes:
- ```[InjectComponent]``` - Checks and caches the component added to game object.
- ```[InjectComponentFromChild("Child name")]``` - Checks and caches the component added to the child object with the name "Child name" on the game object.
- ```[InjectComponentFromObject("Object name")]``` - Checks and caches a component added to another game object.
- ```[InjectAsSingle]``` - Сreates an instance of the class as a singleton.
- ```[InjectAsTransient]``` - Сreates instances of the class.

3. When you want to initialize the prefab, you need to use DIService
```C#
DIService.InstantiateAndInject(prefab);
```
