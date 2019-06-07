namespace Tests
{

  public enum Gender
  {
    Female,
    Male
  }
  public class Sample
  {
    public int id { get; set; }
    public string name { get; set; }
    public bool isValid { get; set; }
    public Gender gender { get; set; }
    public decimal age { get; set; }
  }
}