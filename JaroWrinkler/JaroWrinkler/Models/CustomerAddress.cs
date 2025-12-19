using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JaroWrinklerSimilarity.Models
{
    [Table("CUSTOMER_ADDRESS")]
    public class CustomerAddress
    {
        [Key]
        [Column("EXTRNL_CLIENT_ID")]
        [MaxLength(50)]
        public string EXTRNL_CLIENT_ID { get; set; } = null!;

        [Column("CUST_PROF_DESIG")]
        [MaxLength(50)]
        public string? CUST_PROF_DESIG { get; set; }

        [Column("CUST_SPECIALTY")]
        [MaxLength(100)]
        public string? CUST_SPECIALTY { get; set; }

        [Column("CUST_FIRST_NAME")]
        [MaxLength(100)]
        public string? CUST_FIRST_NAME { get; set; }

        [Column("CUST_LAST_NAME")]
        [MaxLength(100)]
        public string? CUST_LAST_NAME { get; set; }

        [Column("EXTRNL_CLIENT_LOCID")]
        [MaxLength(50)]
        public string? EXTRNL_CLIENT_LOCID { get; set; }

        [Column("CUST_INACTIVE_ADDR")]
        public int? CUST_INACTIVE_ADDR { get; set; }

        [Column("CUST_ADDRESS1")]
        [MaxLength(300)]
        public string? CUST_ADDRESS1 { get; set; }

        [Column("CUST_ADDRESS2")]
        [MaxLength(300)]
        public string? CUST_ADDRESS2 { get; set; }

        [Column("CUST_CITY")]
        [MaxLength(150)]
        public string? CUST_CITY { get; set; }

        [Column("CUST_STATE")]
        [MaxLength(50)]
        public string? CUST_STATE { get; set; }

        [Column("ADDR_DIGITAL_ID")]
        [MaxLength(50)]
        public string? ADDR_DIGITAL_ID { get; set; }

        [Column("ADDR_SAMPLEABLE_FLAG")]
        public int? ADDR_SAMPLEABLE_FLAG { get; set; }

        [Column("VERIFIED_DATE")]
        public DateTime? VERIFIED_DATE { get; set; }

        [Column("STATE_CS_NONSAMPLE_ELIG_STATUS")]
        [MaxLength(50)]
        public string? STATE_CS_NONSAMPLE_ELIG_STATUS { get; set; }

        [Column("CS_ELIG_STATUS")]
        [MaxLength(50)]
        public string? CS_ELIG_STATUS { get; set; }

        [Column("DEA")]
        [MaxLength(50)]
        public string? DEA { get; set; }

        [Column("DEA_SCHEDULE")]
        [MaxLength(50)]
        public string? DEA_SCHEDULE { get; set; }

        [Column("DEA_STATUS")]
        [MaxLength(50)]
        public string? DEA_STATUS { get; set; }

        [Column("DEA_EXP_DATE")]
        public DateTime? DEA_EXP_DATE { get; set; }
    }
}
