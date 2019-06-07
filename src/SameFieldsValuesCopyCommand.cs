namespace ObjectFields
{
  public interface SameFieldsValuesCopyCommand : Command
  {
    void setSource(object value);
    void setDestination(object value);
  }
}
