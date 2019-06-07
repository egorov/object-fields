namespace ObjectFields
{
  public class BuiltInTypesEqualityComparerImpl : BuiltInTypesEqualityComparer
  {
    public bool areEquals(object left, object right)
    {
      if (left != null)
        if (left.Equals(right))
          return true;

      if (right != null)
        if (right.Equals(left))
          return true;

      return false;
    }
  }
}
