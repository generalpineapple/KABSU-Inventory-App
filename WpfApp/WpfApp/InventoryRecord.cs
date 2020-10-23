using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    class InventoryRecord
    {
        private string item;
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
        public InventoryRecord(string item, string description, string qty, string whatINeed, string Notes, string id)
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
