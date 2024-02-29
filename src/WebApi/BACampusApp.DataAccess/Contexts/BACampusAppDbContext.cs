using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BACampusApp.DataAccess.Contexts;
public class BACampusAppDbContext : IdentityDbContext<IdentityUser,IdentityRole,string>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    internal const string ConnectionName = "BACampusApp";
    public BACampusAppDbContext(DbContextOptions<BACampusAppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options) 
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<Trainer> Trainers { get; set; }
   
    public virtual DbSet<Question> Questions { get; set; }
    public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
    public virtual DbSet<Exam> Exams { get; set; }
    public virtual DbSet<QuestionTopic> QuestionTopics { get; set; }
    public virtual DbSet<StudentExam> Students_Exams { get; set; }
    public virtual DbSet<StudentQuestion> Students_Questions { get; set; }
    public virtual DbSet<StudentAnswer> Students_Answers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityTypeConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        SetBaseProperties();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetBaseProperties();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetBaseProperties()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        var user = _httpContextAccessor.HttpContext!.User;
        var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        foreach (var entry in entries)
        {
            SetIfAdded(entry, userId);
            SetIfModified(entry, userId);
            SetIfDeleted(entry, userId);
        }
    }

    private void SetIfDeleted(EntityEntry<BaseEntity> entry, string userId)
    {
        if (entry.State is not EntityState.Deleted)
        {
            return;
        }

        if (entry.Entity is not AuditableEntity entity)
        {
            return;
        }

        entry.State = EntityState.Modified;

        entity.Status = Status.Deleted;
        entity.DeletedDate = DateTime.Now;
        entity.DeletedBy = userId;
    }

    private void SetIfModified(EntityEntry<BaseEntity> entry, string userId)
    {
        if (entry.State == EntityState.Modified)
        {
            entry.Entity.Status = Status.Active;
        }

        entry.Entity.ModifiedBy = userId;
        entry.Entity.ModifiedDate = DateTime.Now;
    }

    private async void SetIfAdded(EntityEntry<BaseEntity> entry, string userId)
    {
        if (entry.State != EntityState.Added)
        {
            return;
        }
     
        entry.Entity.Status = Status.Active;
        entry.Entity.CreatedBy = userId;
        entry.Entity.CreatedDate = DateTime.Now;
    }
}