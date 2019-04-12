using System;
using System.IO;
using System.Reflection;

namespace Chatman
{
    public class Save : Attribute
    {
        public void toFile(Object obj)
        {
            string output = "";
            
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(propertyInfo))
                {
                    if (attr.GetType() == typeof(Save))
                    {
                        output += " " + propertyInfo.GetValue(obj);
                    }
                }
            }
            
            using (StreamWriter writetext = new StreamWriter(obj.ToString() + ".txt"))
            {
                writetext.WriteLine(output);
            }
        }

        public void fromFile(Object obj)
        {
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(propertyInfo))
                {
                    if (attr.GetType() != typeof(Save)) continue;
                    using (StreamReader readtext = new StreamReader("monFichier.txt"))
                    {
                        propertyInfo.SetValue(obj, readtext.ReadLine());
                    }
                }
            }
        }
    }
}