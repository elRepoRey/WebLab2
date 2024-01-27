import express, { Request, Response } from 'express';
import FileServicesRoutes from './Routes/FileServicesRoutes';
import ProductServicesRoutes from './Routes/ProductServicesRoutes';
import cors from 'cors';
import dotenv from 'dotenv';
dotenv.config();

const app = express();
const port =  3000;

app.use(cors({ origin: process.env.CLIENT_URL }));
app.use(express.json());
app.use('/FileServices', FileServicesRoutes);
app.use('/ProductServices', ProductServicesRoutes);


app.listen(port, () => console.log(`NodeService listening on port ${port}!`));
