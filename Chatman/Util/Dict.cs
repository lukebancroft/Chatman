using System;
using System.Collections.Generic;
using System.Linq;

namespace Chatman
{
    public class Dict
    {
        private List<int> keys;
        private List<string> values;

        public Dict(List<int> keys = null, List<string> values = null)
        {
            if (keys != null && values != null)
            {
                this.keys = keys;
                this.values = values;
            }
            else
            {
                this.keys = new List<int>();
                this.values = new List<string>();
            }
        }

        public string this[int index]
        {
            get
            {
                int position = keys.IndexOf(index);

                if (position != -1)
                {
                    return values[position];
                }

                return null;
            }
        }

        public bool add(int key, string value)
        {
            if (keys.IndexOf(key) == -1)
            {
                keys.Add(key);
                values.Add(value);
                return true;
            }

            return false;
        }

        public bool remove(int key)
        {
            int position = keys.IndexOf(key);
            
            if (position != -1)
            {
                keys.RemoveAt(position);
                values.RemoveAt(position);
                return true;
            }

            return false;
        }

        public void display()
        {
            for (int i = 0; i < keys.Count; i++)
            {
                Console.WriteLine(keys[i] + ";" + values[i]);
            }
        }

        public List<int> Keys
        {
            get => keys;
            set => keys = value;
        }

        public List<string> Values
        {
            get => values;
            set => values = value;
        }
    }
}