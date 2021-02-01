using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    /// <summary>
    /// class for the fields in additional window
    /// </summary>
    class Morph
    {
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
        private string vigor;
        /// <summary>
        /// Property for vigor
        /// </summary>
        public string Vigor
        {
            get
            {
                return this.vigor;
            }
            set
            {
                this.vigor = value;
            }
        }
        private string mot;
        /// <summary>
        /// Property for mot
        /// </summary>
        public string Mot
        {
            get
            {
                return this.mot;
            }
            set
            {
                this.mot = value;
            }
        }
        private string morph;
        /// <summary>
        /// Property for morph
        /// </summary>
        public string Morphology
        {
            get
            {
                return this.morph;
            }
            set
            {
                this.morph = value;
            }
        }
        private string code;
        /// <summary>
        /// Property for code.
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
        private string units;
        /// <summary>
        /// Property for units
        /// </summary>
        public string Units
        {
            get
            {
                return this.units;
            }
            set
            {
                this.units = value;
            }
        }
        private string id;
        /// <summary>
        /// Property for id
        /// </summary>
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// constructor when fields aren't empty
        /// </summary>
        /// <param name="notes"></param>
        /// <param name="date"></param>
        /// <param name="vigor"></param>
        /// <param name="mot"></param>
        /// <param name="morph"></param>
        /// <param name="code"></param>
        /// <param name="units"></param>
        /// <param name="id"></param>
        public Morph(string notes, string date, string vigor, string mot, string morph, string code, string units, string id)
        {
            this.notes = notes;
            this.date = date;
            this.vigor = vigor;
            this.mot = mot;
            this.morph = morph;
            this.code = code;
            this.units = units;
            this.id = id;
        }

        /// <summary>
        /// construcor when fields are empty
        /// </summary>
        public Morph()
        {
            this.notes = "";
            this.date = "";
            this.vigor = "";
            this.mot = "";
            this.morph = "";
            this.code = "";
            this.units = "";
            this.id = "";
        }
    }
}
