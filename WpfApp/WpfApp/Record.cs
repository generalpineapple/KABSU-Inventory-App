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
    /// class to hold information for each record
    /// </summary>
    class Record
    {
        private string toFrom;
        /// <summary>
        /// Property for toFrom
        /// </summary>
        public string ToFrom
        {
            get
            {
                return this.toFrom;
            }
            set
            {
                this.toFrom = value;
            }
        }
        private string date;
        /// <summary>
        /// Property for date
        /// </summary>
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
        }
        private string rec;
        /// <summary>
        /// Property for rec
        /// </summary>
        public string Rec
        {
            get
            {
                return this.rec;
            }
            set
            {
                this.rec = value;
            }
        }
        private string ship;
        /// <summary>
        /// Property for ship
        /// </summary>
        public string Ship
        {
            get
            {
                return this.ship;
            }
            set
            {
                this.ship = value;
            }
        }
        private string balance;
        /// <summary>
        /// Property for balance
        /// </summary>
        public string Balance
        {
            get
            {
                return this.balance;
            }
            set
            {
                this.balance = value;
            }
        }
        private string animalId;
        /// <summary>
        /// Property for animalID
        /// </summary>
        public string AnimalId
        {
            get
            {
                return this.animalId;
            }
            set
            {
                this.animalId = value;
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="toFrom"></param>
        /// <param name="date"></param>
        /// <param name="rec"></param>
        /// <param name="ship"></param>
        /// <param name="balance"></param>
        /// <param name="id"></param>
        public Record(string toFrom, string date, string rec, string ship, string balance, string id)
        {
            this.toFrom = toFrom;
            this.date = date;
            this.rec = rec;
            this.ship = ship;
            this.balance = balance;
            this.animalId = id;
        }
    }
}
