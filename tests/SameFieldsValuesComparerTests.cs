using ObjectFields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
  [TestClass]
  public class SameFieldsValuesComparerTests
  {
    private SameFieldsValuesComparer compare;

    [TestInitialize]
    public void setUp()
    {
      this.compare = new SameFieldsValuesComparerImpl();
    }

    [TestMethod]
    public void should_return_true()
    {
      Sample standard = new Sample();
      Sample sample = new Sample();

      Assert.IsTrue(this.compare.areEquals(standard, sample));
    }

    [TestMethod]
    public void should_return_false()
    {
      Sample standard = new Sample() { name = "jack" };
      Sample sample = new Sample();

      Assert.IsFalse(this.compare.areEquals(standard, sample));
    }
  }
}
