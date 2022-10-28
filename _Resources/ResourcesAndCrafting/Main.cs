using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public class ItemBase
{
}

public class Item : ItemBase
{
    public string name;
    public string description;

    public Item()
    {
        name = "";
        description = "";
    }

    public Item(string name, string description)
    {
        this.name = name;
        this.description = description;
    }
}

public class ItemStack : ItemBase
{
    public Item item;
    public uint quantity;

    public ItemStack()
    {
        item = new Item();
        quantity = 0;
    }

    public ItemStack(Item item, uint quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}

public class Recipe
{
    public ItemBase result;
    public List<ItemBase> ingredients;

    public Recipe()
    {
        result = new ItemBase();
        ingredients = new List<ItemBase>();
    }

    public Recipe(ItemBase result, List<ItemBase> ingredients)
    {
        this.result = result;
        this.ingredients = ingredients;
    }

    public Recipe(ItemBase result, ItemBase ingredient, params ItemBase[] additionalIngredients)
    {
        this.result = result;
        this.ingredients = new List<ItemBase>();
        this.ingredients.Add(ingredient);
        this.ingredients.AddRange(additionalIngredients);
    }
}

public class LimitedRecipe : Recipe
{
    public uint uses;

    public LimitedRecipe()
        : base()
    {
        uses = 0;
    }

    public LimitedRecipe(ItemBase result, List<ItemBase> ingredients, uint uses)
        : base(result, ingredients)
    {
        this.uses = uses;
    }

    public LimitedRecipe(uint uses, ItemBase result, ItemBase ingredient, params ItemBase[] additionalIngredients)
        : base(result, ingredient, additionalIngredients)
    {
        this.uses = uses;
    }
}

[XmlInclude(typeof(Item)), XmlInclude(typeof(ItemStack)), XmlInclude(typeof(LimitedRecipe))]
public class RecipeBook
{
    public List<Recipe> recipes;

    public RecipeBook()
    {
        recipes = new List<Recipe>();
    }

    public RecipeBook(List<Recipe> recipes)
    {
        this.recipes = recipes;
    }

    public RecipeBook(Recipe recipe, params Recipe[] additionalRecipes)
    {
        recipes = new List<Recipe>();
        recipes.Add(recipe);
        recipes.AddRange(additionalRecipes);
    }

    public RecipeBook(string fileName)
    {
        recipes = new List<Recipe>();
        XmlSerializer serializer = new XmlSerializer(typeof(RecipeBook));
        System.IO.StreamReader reader = new System.IO.StreamReader(fileName);
        RecipeBook book = (RecipeBook)serializer.Deserialize(reader);
        reader.Close();
        recipes = book.recipes;
    }

    public static RecipeBook OpenFromFile(string fileName)
    {
        return new RecipeBook(fileName);
    }

    public void SaveToFile(string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(RecipeBook));
        System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName);
        serializer.Serialize(writer, this);
        writer.Close();
    }

    public void SaveToFile()
    {
        GetTimeAndDate(out string time, out string date);
        var fileName = GetSafeFileName($"recipe_book_{date}_{time}.xml");
        SaveToFile(fileName);
    }

    private void GetTimeAndDate(out string time, out string date)
    {
        time = DateTime.Now.ToString("HH-mm-ss");
        date = DateTime.Now.ToString("dd-MM-yyyy");
    }

    private string IncrementFileName (string fileName, uint index)
    {
        return $"{Path.GetFileNameWithoutExtension(fileName)}_{index}{Path.GetExtension(fileName)}";
    }

    private string GetSafeFileName(string fileName)
    {
        if (File.Exists(fileName))
        {
            uint i = 1;
            string newFileName;
            for (newFileName = IncrementFileName(fileName, i); File.Exists(newFileName); newFileName = IncrementFileName(fileName, ++i));
            fileName = newFileName;
        }
        return fileName;
    }

    public void DumpToConsole()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Recipe recipe in recipes)
        {
            sb.AppendLine("Recipe\n{");
            sb.AppendLine("  Result\n  {");
            
            // if the Item is a valid ItemStack, dump the quantity
            if (recipe.result is Item)
            {
                var item = (Item)recipe.result;

                sb.AppendLine($"    name: {item.name}");
                sb.AppendLine($"    description: {item.description}");
            }
            if (recipe.result is ItemStack)
            {
                var itemStack = (ItemStack)recipe.result;

                sb.AppendLine($"    name: {itemStack.item.name}");
                sb.AppendLine($"    description: {itemStack.item.description}");
                sb.AppendLine($"    quantity: {itemStack.quantity}");
            }
            sb.AppendLine($"  },");
            sb.AppendLine("  Ingredients\n  [");
            foreach (ItemBase ingredient in recipe.ingredients)
            {
                sb.AppendLine("    Ingredient\n    {");
                if (ingredient is Item)
                {
                    var item = (Item)ingredient;

                    sb.AppendLine($"      name: {item.name}");
                    sb.AppendLine($"      description: {item.description}");
                }
                else if (ingredient is ItemStack)
                {
                    var itemStack = (ItemStack)ingredient;

                    sb.AppendLine($"      name: {itemStack.item.name}");
                    sb.AppendLine($"      description: {itemStack.item.description}");
                    sb.AppendLine($"      quantity: {itemStack.quantity}");
                }
                else
                {
                    sb.AppendLine("      Invalid ingredient");
                }
                sb.Append("    },");
            }
            // Remove the last comma of list
            sb.Length--;
            sb.Append("\n  ],");
            // if the Recipe is a valid LimitedRecipe, dump the uses
            if (recipe is LimitedRecipe)
            {
                sb.AppendLine($"\n  uses: {((LimitedRecipe)recipe).uses}");
            }
            else
            {
                sb.Length--;
                sb.Append("\n");
            }
            sb.AppendLine("}");
        }

        Console.WriteLine(sb.ToString());
    }

    public void AddRecipe(Recipe recipe)
    {
        recipes.Add(recipe);
    }
}

public class ItemBook
{
    // Raw Resources
    public static Item Copper = new Item("Copper", "A piece of copper (Cu) ore.");
    public static Item Zinc = new Item("Zinc", "A piece of zinc (Zn) ore.");
    public static Item Lead = new Item("Lead", "A piece of lead (Pb) ore.");
    public static Item Titanium = new Item("Titanium", "A piece of titanium (Ti) ore.");
    public static Item Uraninite = new Item("Uraninite", "A mineral rich with uranium (U).");
    public static Item NitricAcid = new Item("Nitric Acid", "A glass container of nitric acid (HNO3).");
    public static Item SulfuricAcid = new Item("Sulfuric Acid", "A glass container of sulfuric acid (H2SO4).");
    public static Item Cellulose = new Item("Cellulose", "A pile of cellulose (C6H7(OH)3O2).");

    // Processed Resources
    public static Item Brass = new Item("Brass", "A brass ingot an alloy metal made from copper and zinc.");
    public static Item CopperWire = new Item("Copper Wire", "A spool of copper wire.");
    public static Item Solenoid = new Item("Solenoid", "A solenoid made from coils of copper wire.");
    public static Item SmokelessPowder = new Item("Smokeless Powder", "A single base smokeless powder made of nitrocellulose (C6H7(ONO2)3O2) made from nitric and sulfuric acid as well as cellulose (3 HNO3 + C6H7(OH)3O2 (→ H2SO4 →) C6H7(ONO2)3O2 + 3 H2O).");

    // Ammunition
    public static Item RifleMagazine = new Item("Rifle Magazine", "A magazine for a rifle.");
    public static Item RifleCartridge = new Item("Rifle Cartridge", "A cartridge for a rifle.");

    // Firearms
    public static Item Rifle = new Item("Rifle", "A basic rifle.");
}

namespace Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RecipeBook recipeBook = new RecipeBook
            (
                new Recipe( new ItemStack(ItemBook.Brass, 3), new ItemStack(ItemBook.Copper, 2), new ItemStack(ItemBook.Zinc, 1) ),
                new Recipe( new ItemStack(ItemBook.SmokelessPowder, 5), new ItemStack(ItemBook.Cellulose, 1), new ItemStack(ItemBook.NitricAcid, 3), new ItemStack(ItemBook.SulfuricAcid, 1) ),
                new Recipe( new ItemStack(ItemBook.CopperWire, 1), new ItemStack(ItemBook.Copper, 3) ),
                new Recipe( new ItemStack(ItemBook.Solenoid, 1), new ItemStack(ItemBook.CopperWire, 2) ),
                new Recipe( new ItemStack(ItemBook.RifleMagazine, 1), new ItemStack(ItemBook.Titanium, 2) ),
                new Recipe( new ItemStack(ItemBook.RifleCartridge, 10), new ItemStack(ItemBook.SmokelessPowder, 1), new ItemStack(ItemBook.Copper, 1), new ItemStack(ItemBook.Lead, 1) ),
                new LimitedRecipe( 1, ItemBook.Rifle, new ItemStack(ItemBook.Titanium, 12), new ItemStack(ItemBook.Copper, 2) )
            );

            recipeBook.DumpToConsole();
            recipeBook.SaveToFile();
        }
    }
}