/*
 * Visual Studio 2019
 --------------------------------------------------------
-<<copyright file-"AdditionalInfo.cs"-company=KABSU>"
------Copyright-statement.-All-right-reserved
-</copyright>
 --------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    /// <summary>
    /// class that pulls in field data from a user's search.
    /// </summary>
    public class SearchTerm
    {
        private string owner;
        /// <summary>
        /// Property for owner
        /// </summary>
        public string Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                this.owner = value;
            }
        }
        private string breed;
        /// <summary>
        /// Property for breed
        /// </summary>
        public string Breed
        {
            get
            {
                return this.breed;
            }
            set
            {
                this.breed = value;
            }
        }
        private string animalName;
        /// <summary>
        /// Property for animalName
        /// </summary>
        public string AnimalName
        {
            get
            {
                return this.animalName;
            }
            set
            {
                this.animalName = value;
            }
        }
        private string code;
        /// <summary>
        /// Property for code
        /// </summary>
        public string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }
        private string canNum;
        /// <summary>
        /// Property for canNum
        /// </summary>
        public string CanNum
        {
            get
            {
                return this.canNum;
            }
            set
            {
                this.canNum = value;
            }
        }
        private string town;
        /// <summary>
        /// Property for town
        /// </summary>
        public string Town
        {
            get
            {
                return this.town;
            }
            set
            {
                this.town = value;
            }
        }
        private string state;
        /// <summary>
        /// Property for state
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
        /*private string date;
        public string Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
            }
        }*/
        /// <summary>
        /// Constructor for the object, taking in search fields from the Search Window
        /// </summary>
        /// <param name="canNum">The cane number of the animal</param>
        /// <param name="code">The unique id of the animal</param>
        /// <param name="animalName">The name of the animal</param>
        /// <param name="breed">A breed of animal</param>
        /// <param name="owner">The owner of an animal</param>
        /// <param name="town">The town of origin</param>
        /// <param name="state">The state of origin</param>
        public SearchTerm(string canNum, string code, string animalName, string breed, string owner, string town, string state)
        {
            this.CanNum = canNum;
            this.Code = code;
            this.AnimalName = animalName;
            this.Breed = breed;
            this.Owner = owner;
            this.Town = town;
            this.State = state;
            //this.Date = date;
        }
    }
}
