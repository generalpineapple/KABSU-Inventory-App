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
        private string rate;
        public string Rate
        {
            get
            {
                return this.rate;
            }
            set
            {
                this.rate = value;
            }
        }
        private string amount;
        public string Amount
        {
            get
            {
                return this.amount;
            }
            set
            {
                this.amount = value;
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
        public InventoryRecord(string item, string description, string qty, string rate, string amount, string id)
        {
            this.item = item;
            this.description = description;
            this.qty = qty;
            this.rate = rate;
            this.amount = amount;
            this.animalId = id;
        }
    }
}
