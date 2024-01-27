// FileServicesRoutes.js
import express from 'express';
import multer from 'multer';
import axios from 'axios';
import FormData from 'form-data';
import https from 'https';
import dotenv from 'dotenv';
dotenv.config();

const router = express.Router();
const upload = multer();

const axiosInstance = axios.create({
    httpsAgent: new https.Agent({  
        rejectUnauthorized: false 
    })
});

const ASPNET_API_URL_FILESERVICES = process.env.AZURE_THEFLOWERPLACEAPI_URL

// list all files
router.get('/Files', async (req, res) => {
    try {
        const response = await axiosInstance.get(`${ASPNET_API_URL_FILESERVICES}/Files?containerName=files`);
        res.json(response.data);
    } catch (error) {
        res.status(500).send('Error listing files');
    }
});

//upload a file
router.post('/File', upload.single('file'), async (req, res) => {
    const { containerName } = req.query;
   
    if (!req.file) {
        return res.status(400).send('No file uploaded.');
    }

    const formData = new FormData();
    formData.append('file', req.file.buffer, req.file.originalname);

    try {
        const response = await axiosInstance.post(`${ASPNET_API_URL_FILESERVICES}/File?containerName=${containerName}`, formData, {
            headers: formData.getHeaders()
        });
        res.json(response.data);
    } catch (error: any) {
        console.error(error);
        res.status(500).json({ message: 'Error uploading file', error: error.message });
    }
});

// delete a file
router.delete('/File', async (req, res) => {
    const { containerName, fileName } = req.query;
    try {
        await axiosInstance.delete(`${ASPNET_API_URL_FILESERVICES}/File?containerName=${containerName}&fileName=${fileName}`);
        res.json('File deleted');
    } catch (error) {
        res.status(500).send('Error deleting file');
    }
});

export default router;
