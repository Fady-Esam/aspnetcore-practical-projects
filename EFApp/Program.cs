


using EFApp;

class Program
{
    static void Main()
    {
        #region GetData
        //using (var context = new AppDBContext())
        //{
        //    var dat = context.geneTypes.FirstOrDefault(x => x.GeneTypeID == 1);
        //    Console.WriteLine(dat?.GeneName);
        //    foreach(var item in context.geneTypes)
        //    {
        //        Console.WriteLine(item.GeneName);
        //    }
        //}
        #endregion

        #region Insert Data
        //var GeneType = new GeneTypes
        //{
        //    GeneName = "Test2"
        //};
        //using (var context = new AppDBContext())
        //{
        //    context.Add(GeneType);
        //    context.SaveChanges();
        //}

        #endregion
        #region Update Data

        //using(var context = new AppDBContext())
        //{
        //    var GeneType = context.geneTypes.Single(x => x.GeneTypeID == 25);
        //    GeneType.GeneName = "Update Gene";
        //    context.SaveChanges();
        //}
        #endregion
        #region Delete Data
        using (var context = new AppDBContext())
        {
            var GeneType = context.geneTypes.Single(x => x.GeneTypeID == 25);
            context.Remove(GeneType);
            context.SaveChanges();
        }
        #endregion
        Console.ReadKey();  
    }
}






