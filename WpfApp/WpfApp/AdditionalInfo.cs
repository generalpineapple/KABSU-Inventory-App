using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class AdditionalInfo
    {

        private DateTime lastModified;
        public DateTime LastModified
        {
            get
            {
                return this.lastModified;
            }
            set
            {
                this.lastModified = value;
            }
        }

        private string species;
        public string Species
        {
            get
            {
                return this.species;
            }
            set
            {
                this.species = value;
            }
        }
        private string city;
        public string City
        {
            get
            {
                return this.city;
            }
            set
            {
                this.city = value;
            }
        }
        private string state;
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }
        private string country;
        public string Country
        {
            get
            {
                return this.country;
            }
            set
            {
                this.country = value;
            }
        }
        public AdditionalInfo(string species, string city, string state, string country)
        {
            this.species = species;
            this.city = city;
            this.state = state;
            this.country = country;
        }

        public AdditionalInfo()
        {
            this.species = "";
            this.city = "";
            this.state = "";
            this.country = "";
        }
    }
}
