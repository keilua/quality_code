namespace CodeQuality.Samples.Legacy.GildedRose;

/// <summary>
/// 
/// </summary>
public class GildedRose
{
    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            if (ItemNameNotAgeBrie(i) && ItemNameNotBackstage(i))
            {
                if (ItemQualitysup0(i))
                {
                    if (ItemNameNotSulfuras(i))
                    {
                        ItemQualityDowngradeby1(i);
                    }
                }
            }
            else
            {
                if (ItemQualitycheckinfby50(i))
                {
                    ItemQualityUpgradeby1(i);

                    if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (Items[i].SellIn < 11)
                        {
                            if (ItemQualitycheckinfby50(i))
                            {
                                ItemQualityUpgradeby1(i);
                            }
                        }

                        if (Items[i].SellIn < 6)
                        {
                            if (ItemQualitycheckinfby50(i))
                            {
                                ItemQualityUpgradeby1(i);
                            }
                        }
                    }
                }
            }

            if (ItemNameNotSulfuras(i))
            {
                Items[i].SellIn = Items[i].SellIn - 1;
            }

            if (Items[i].SellIn < 0)
            {
                if (ItemNameNotAgeBrie(i))
                {
                    if (ItemNameNotBackstage(i))
                    {
                        if (ItemQualitysup0(i))
                        {
                            if (ItemNameNotSulfuras(i))
                            {
                                ItemQualityDowngradeby1(i);
                            }
                        }
                    }
                    else
                    {
                        Items[i].Quality = Items[i].Quality - Items[i].Quality;
                    }
                }
                else
                {
                    if (ItemQualitycheckinfby50(i))
                    {
                        ItemQualityUpgradeby1(i);
                    }
                }
            }
        }
    }

    private bool ItemQualitysup0(int i)
    {
        return Items[i].Quality > 0;
    }

    private bool ItemNameNotAgeBrie(int i)
    {
        return Items[i].Name != "Aged Brie";
    }

    private bool ItemNameNotBackstage(int i)
    {
        return Items[i].Name != "Backstage passes to a TAFKAL80ETC concert";
    }

    private bool ItemNameNotSulfuras(int i)
    {
        return Items[i].Name != "Sulfuras, Hand of Ragnaros";
    }

    private bool ItemQualitycheckinfby50(int i)
    {
        return Items[i].Quality < 50;
    }

    private void ItemQualityDowngradeby1(int i)
    {
        Items[i].Quality = Items[i].Quality - 1;
    }

    private void ItemQualityUpgradeby1(int i)
    {
        Items[i].Quality = Items[i].Quality + 1;
    }
}