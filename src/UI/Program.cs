internal class Program
{
    
    private static void Main(string[] args)
    {

        var json2 = @"{
    '0': {
        'position': 1,
        'name': 'Mediterranean Avenue',
        'type': 'RealEstate',
        'color': 'Brown',
        'price': 60,
        'rents': [2, 4, 10, 30, 90, 160, 250],
        'mortgageValue': 30,
        'buildingCost': 50
    },
    '1': {
        'position': 5,
        'name': 'Reading Railroad',
        'type': 'RailRoad',
        'price': 200,
        'rents': [25, 50, 100, 200],
        'mortgageValue': 100
    },
    '2': {
        'position': 12,
        'name': 'Electric Company',
        'type': 'Utility',
        'price': 150,
        'rents': [4, 10],
        'mortgageValue': 75
    },
    '3': {
        'position': 10,
        'name': 'Jail',
        'type': 'Jail',
        'jailFine': 50
    },
    '4': {
        'position': 0,
        'name': 'Go!',
        'type': 'Go',
        'salary': 200
    },
    '5': {
        'position': 4,
        'name': 'Income Tax',
        'type': 'IncomeTax',
        'tax': 200,
        'percentageTax': 10
    },
    '6': {
        'position': 20,
        'name': 'Free Parking',
        'type': 'FreeParking'
    },
    '7': {
        'position': 7,
        'name': 'Chance',
        'type': 'Chance'
    },
    '8': {
        'position': 2,
        'name': 'Community Chest',
        'type': 'CommunityChest'
    },
    '9': {
        'position': 30,
        'name': 'Go To Jail',
        'type': 'GoToJail'
    },
    '10': {
        'position': 38,
        'name': 'Luxury Tax',
        'type': 'LuxuryTax',
        'tax': 100
    }
}
";
        Console.Write(json2);
        Console.ReadKey();
    }


}
