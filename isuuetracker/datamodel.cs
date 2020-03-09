using System.ComponentModel.DataAnnotations.Schema;
namespace isuuetracker
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class datamodel : DbContext
    {
        // Your context has been configured to use a 'datamodel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'isuuetracker.datamodel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'datamodel' 
        // connection string in the application configuration file.
        public datamodel()
            : base("pro")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<login> logins { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<project> projects { get; set; }
        public virtual DbSet<bugpool> bugpools { get; set; }
        public virtual DbSet<history> historys { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
    public class login
    {
        [Key]
        public int loginId { get; set; }
        [Required]
        [StringLength(30)]
        public string username { get; set; }
        [Required]
        [StringLength(20)]
        public string password { get; set; }
    }
    public class role
    {
        [Key]
        public int rid { get; set; }
        public login ruser { get; set; }
        [Required]
        [ForeignKey("ruser")]
        public int userid { get; set; }
        public project proj { get; set; }
        [Required]
        [ForeignKey("proj")]
        public int projectid { get; set; }
        [Required]
        [StringLength(10)]
        public string work { get; set; }
    }
    public class project
    {
        [Key]
        public int projectid { get; set; }
        [Required]
        public string projectname { get; set; }
       
    }
    public class bugpool
    {
        [Key]
        public int bugid { get; set; }
        
        public login tester { get; set; }

        [Required]
        [ForeignKey("tester")]
        public int testerid { get; set; }

        public project project { get; set; }
        [ForeignKey("project")]
        public int projectid { get; set; }
        [Required]
        [StringLength(30)]
        public string bugname { get; set; }
        [Required]
        [StringLength(10)]
        public string bugtype { get; set; }
        [Required]
        [StringLength(10)]
        public string status { get; set; }
       

        
        public int assigntoId { get; set; }
    }
    public class history
    {
        [Key]
        public int historyid { get; set; }
        
        public bugpool bug { get; set; }
        [ForeignKey("bug")]
        public int bugid { get; set; }

        public login Modifieduser { get; set; }
        [ForeignKey("Modifieduser") ]
        public int ModifieduserId { get; set; }

        [Required]
        [StringLength(100)]
        public string comment { get; set; }
        [Required]
        [StringLength(10)]
        public string status { get; set; }
        [Required]
        public DateTime time { get; set; }
    }

}