using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    /// <summary>
    /// class that keeps track of all the additional info fields used
    /// </summary>
    public class AdditionalInfo
    {
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

        /// <summary>
        /// constructor when the fields are filled out to saver
        /// </summary>
        /// <param name="species"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        public AdditionalInfo(string species, string city, string state, string country)
        {
            this.species = species;
            this.city = city;
            this.state = state;
            this.country = country;
        }

        /// <summary>
        /// constructor for empty fields
        /// </summary>
        public AdditionalInfo()
        {
            this.species = "";
            this.city = "";
            this.state = "";
            this.country = "";
        }
    }
}
