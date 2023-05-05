# DIComponents

DIComponents is a dependency injection framework created for unity 3d. It is used to check and inject component dependencies in unity scripts and does not contain containers for dependency injection. It is simply a substitute for the functionality of unity 3D, such as the GetComponent, transform.find, etc.

# Usage
1. Add an empty game object to the scene and add the MonoDIComponentsInitializer script. This script is used to inject dependencies for objects on the scene.

2. In unity scripts inherited from monoBehavior, add attributes to the fields for caching components.
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
- ```[InjectAsSingle]``` - Сreates instance of the class as a singleton.
- ```[InjectAsTransient]``` - Сreates instances of the class.
- ```[InjectFactory]``` - Creates instance of the factory as a singleton 

3. When you want to initialize the prefab, you need to use DIService
```C#
DIService.InstantiateAndInject(prefab);
```

# Usage factories
1. Сreate a class custom factory that inherits from the class factory. As generic types, specify the types of parameters that will be inserted as parameters in the constructor. Factories are used to create instances of classes (not inherited from MonoBehaviour), attributes are also injected when creating

```C#
public class Enemy
{
    public Enemy(int id, Vector3 pos)
    {
        ...
    }
}

public class EnemyFactory : Factory<int, Vector3, EnemyClass>
{
    public EnemyFactory(IObjectActivator objectActivator) : base(objectActivator) { }
}
```

2. Add attribute where factory will be used.

```C#
public class ExampleClass : MonoBehaviour
{
  [InjectFactory] private EnemyFactory factory;
  
  public void Create()
  {
      var enemy = factory.Create(1, new Vector3(0.1f, 0.2f, 0f));
  }
  ...
}
```
