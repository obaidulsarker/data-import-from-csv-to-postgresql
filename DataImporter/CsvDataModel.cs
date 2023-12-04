using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace DataImporter
{
    public class DataColumn : System.Attribute
    {
        private bool bolIsDataColumn = false;

        public DataColumn(bool status)
        {
            this.bolIsDataColumn = status;
        }

        public bool IsDataColumn()
        {
            return bolIsDataColumn;
        }
    }


    [Table("Device", Schema = "App")]
    public class CsvDataModel 
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Longitude")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double longitude { get; set; }

        [Display(Name = "Latitude")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double latitude { get; set; }

        [Display(Name = "Date")]
        [Required]
        [DataMember, DataColumn(true)]
        public String record_date { get; set; }

        [Display(Name = "Time")]
        [Required]
        [DataMember, DataColumn(true)]
        public String record_time { get; set; }

        [Display(Name = "direct_inclined")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double direct_inclined { get; set; }

        [Display(Name = "diffuse_inclined")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double diffuse_inclined { get; set; }

        [Display(Name = "reflected")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double reflected { get; set; }

        [Display(Name = "global_inclined")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double global_inclined { get; set; }

        [Display(Name = "direct_horiz")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double direct_horiz { get; set; }

        [Display(Name = "diffuse_horiz")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double diffuse_horiz { get; set; }

        [Display(Name = "global_horiz")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double global_horiz { get; set; }

        [Display(Name = "clear_sky")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double clear_sky { get; set; }

        [Display(Name = "top_of_atmosphere")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double top_of_atmosphere { get; set; }

        [Display(Name = "Code")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 Code { get; set; }

        [Display(Name = "temperature")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double temperature { get; set; }

        [Display(Name = "relative_humidity")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double relative_humidity { get; set; }

        [Display(Name = "pressure")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double pressure { get; set; }

        [Display(Name = "wind_speed")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double wind_speed { get; set; }

        [Display(Name = "wind_direction")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double wind_direction { get; set; }

        [Display(Name = "precision")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double precision { get; set; }

        [Display(Name = "rainfall")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double rainfall { get; set; }

        [Display(Name = "snowfall")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double snowfall { get; set; }

        [Display(Name = "snow_depth")]
        [Required]
        [DataMember, DataColumn(true)]
        public Double snow_depth { get; set; }

    }
}
