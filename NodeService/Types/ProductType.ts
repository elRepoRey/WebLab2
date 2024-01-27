// types/ProductType.js

export class ProductType {
     Id: number;
     Name: string;
     Description: string;
     Price: number;
     Image: string;

    constructor(id: number, name: string , description: string, price: number, image: string) {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.Price = price;
        this.Image = image;
    }
}


