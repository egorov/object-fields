namespace ObjectFields
{
  public interface SameFieldsValuesComparer
  {
    bool areEquals(object standard, object sample);
  }
}
