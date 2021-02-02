using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    /// <summary>
    /// class that keeps track of all the additional info fields used for the Additional Info Window.
    /// </summary>
    public class AdditionalInfo
    {
        private string species;
        /// <summary>
        /// Property for species of the bull.
        /// </summary>
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
        /// <summary>
        /// Property for City related to the Owner field.
        /// </summary>
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
        /// <summary>
        /// Property for state related to the Owner field.
        /// </summary>
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
        /// <summary>
        /// Property for country related to the Owner field.
        /// </summary>
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
        /// constructor when the fields are filled out to save
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
        /// constructor for adding nothing.
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
