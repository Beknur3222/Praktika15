using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika15
{
    using System;
    using System.Reflection;

    class MyClass
    {
        private int privateField = 20;
        public string publicField = "Hello, World!";

        public int PublicProperty { get; set; }
        private string PrivateProperty { get; set; }

        public MyClass()
        {
            PublicProperty = 0;
            PrivateProperty = "Приватное";
        }

        private MyClass(int value)
        {
            PublicProperty = value;
            PrivateProperty = "Приватный конструктор";
        }

        public void PublicMethod()
        {
            Console.WriteLine("Публичный метод вызван");
        }

        private void PrivateMethod()
        {
            Console.WriteLine("Приватный метод вызван");
        }
    }

    class Program
    {
        static void Main()
        {
            Type myClassType = typeof(MyClass);
            Console.WriteLine("Имя класса: " + myClassType.Name);

            Console.WriteLine("\nКонструкторы:");
            foreach (var constructor in myClassType.GetConstructors())
            {
                Console.WriteLine($"  {constructor}");
            }

            Console.WriteLine("\nПоля и свойства:");
            foreach (var field in myClassType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                Console.WriteLine($"  {field.FieldType} {field.Name} ({field.Attributes})");
            }

            foreach (var property in myClassType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                Console.WriteLine($"  {property.PropertyType} {property.Name} ({property.Attributes})");
            }

            Console.WriteLine("\nМетоды:");
            foreach (var method in myClassType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                Console.WriteLine($"  {method.ReturnType} {method.Name} ({method.Attributes})");
            }

            object instance = Activator.CreateInstance(myClassType);
            MyClass myObject = (MyClass)instance;

            Console.WriteLine("\nИзменение значений свойств:");
            myObject.PublicProperty = 10;
            PropertyInfo privatePropertyInfo = myClassType.GetProperty("PrivateProperty", BindingFlags.Instance | BindingFlags.NonPublic);
            privatePropertyInfo.SetValue(instance, "Приватное+");

            Console.WriteLine($"  Публичное: {myObject.PublicProperty}");
            Console.WriteLine($"  Приватное: {privatePropertyInfo.GetValue(instance)}");

            Console.WriteLine("\nВызов метода:");
            MethodInfo publicMethodInfo = myClassType.GetMethod("PublicMethod");
            publicMethodInfo.Invoke(instance, null);

            Console.WriteLine("\nВызов приватного метода:");
            MethodInfo privateMethodInfo = myClassType.GetMethod("PrivateMethod", BindingFlags.Instance | BindingFlags.NonPublic);
            privateMethodInfo.Invoke(instance, null);

            Console.ReadKey();
        }
    }

}
