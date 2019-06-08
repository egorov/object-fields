# Objects fields values tools

**Command** to copy values of fields one object to another. Just fields with same names and types. Like this:

    One one = new One() 
    {
      id = 1,
      name = "jack"
    };
    Another another = new Another();

    SameFieldsValuesCopyCommand command = new SameFieldsValuesCopyCommandImpl();
    command.setSource(one);
    command.setDestination(another);
    command.execute();

    Assert.AreEqual(another.id, one.id);
    Assert.AreEqual(another.name, one.name);

**Tool** to compare values of same name and type fields of different objects instances. Like this:

    One one = new One()
    {
      id = 33,
      name = "john"
    };
    Another another = new Another()
    {
      Id = 150,
      name = "john"
    };
    Another empty = new Another();
    SameFieldsValuesComparer compare = new SameFieldsValuesComparerImpl();

    Assert.IsTrue(compare.areEquals(one, another));
    Assert.IsFalse(compare.areEquals(empty, another));
    Assert.IsFalse(compare.areEquals(empty, one));