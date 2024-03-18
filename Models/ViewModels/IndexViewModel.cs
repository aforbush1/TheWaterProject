namespace TheWaterProject.Models.ViewModels
{
    public class IndexViewModel
    {
        public IQueryable<Project> Projects { get; set;}
        public PaginationInfo PaginationInfo { get; set;} = new PaginationInfo();
    }
}
