using System;

namespace Kaban.Entities
{
    public abstract class HasName : HasIdBase<int>, IHasName
    {
        private string _name;

        protected HasName()
        {
        }

        protected HasName(string name)
        {
            Name = name;
        }
        
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _name = value;
            }
        }
    }
}