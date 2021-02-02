using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    /// <summary>
    /// class for Inventory Record that is being called by the Inventory Page window.
    /// </summary>
    class InventoryRecord
    {
        private string item;
        /// <summary>
        /// Property for item 
        /// </summary>
        public string Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;
            }
        }
        private string description;
        /// <summary>
        /// property for description
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
        private string qty;
        /// <summary>
        /// Property for qty
        /// </summary>
        public string Qty
        {
            get
            {
                return this.qty;
            }
            set
            {
                this.qty = value;
            }
        }
        private string whatINeed;
        /// <summary>
        /// Property for whatINeed
        /// </summary>
        public string WhatINeed
        {
            get
            {
                return this.whatINeed;
            }
            set
            {
                this.whatINeed = value;
            }
        }
        private string notes;
        /// <summary>
        /// Property for notes
        /// </summary>
        public string Notes
        {
            get
            {
                return this.notes;
            }
            set
            {
                this.notes = value;
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
        /// constructor when the fields are filled out.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="description"></param>
        /// <param name="qty"></param>
        /// <param name="whatINeed"></param>
        /// <param name="notes"></param>
        /// <param name="id"></param>
        public InventoryRecord(string item, string description, string qty, string whatINeed, string notes, string id)
        {
            this.item = item;
            this.description = description;
            this.qty = qty;
            this.whatINeed = whatINeed;
            this.notes = notes;
            this.animalId = id;
        }
    }
}
