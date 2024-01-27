// types/ProductType.js

export class ProductType {
    private Id: number;
    private Name: string;
    private Description: string;
    private Price: number;
    private Image: string;

    constructor(id: number, name: string , description: string, price: number, image: string) {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.Price = price;
        this.Image = image;
    }
}


