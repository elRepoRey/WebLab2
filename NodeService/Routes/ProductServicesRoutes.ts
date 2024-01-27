// ProductServicesRoutes.js
import express from 'express';
import axios from 'axios';
import https from 'https';
import  {ProductType}  from '../Types/ProductType';
import dotenv from 'dotenv';
dotenv.config();

const axiosInstance = axios.create({
    httpsAgent: new https.Agent({  
        rejectUnauthorized: false 
    })
});

const router = express.Router();

const NODE_API_URL_PRODUCTSERVICES = process.env.AZURE_THEFLOWERPLACEAPI_URL

// list all products
router.get('/Products', async (req, res) => {
    try {
        const response = await axiosInstance.get(`${NODE_API_URL_PRODUCTSERVICES}/Products`);
        
        res.json(response.data);
    } catch (error) {
        res.status(500).send('Error fetching products');
    }
});

// get a product
router.post('/Product', async (req, res) => {
    try {        
        const { Name, Description, Price, Image } = req.body;  
        const Id = 0;       
        const newProduct = new ProductType(Id, Name, Description, Price, Image);       
        const response = await axiosInstance.post(`${NODE_API_URL_PRODUCTSERVICES}/Product`, newProduct);
        res.json(response.data);
    } catch (error) {
        res.status(500).send('Error adding product');
    }
});

// update a product
router.put('/Product/:id', async (req, res) => {
    try {
        const response = await axiosInstance.put(`${NODE_API_URL_PRODUCTSERVICES}/Product`, req.body);
        res.json(response.data);
    } catch (error) {
        res.status(500).send('Error updating product');
    }
});


// delete a product
router.delete('/Product/:id', async (req, res) => {
    try {
        console.log(req.params.id);
        const response = await axiosInstance.delete(`${NODE_API_URL_PRODUCTSERVICES}/Product?id=${req.params.id}`);
        res.json(response.data);
    } catch (error) {
        res.status(500).send('Error deleting product');
    }
});

export default router;
