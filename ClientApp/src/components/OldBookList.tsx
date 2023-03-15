import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Book from '../models/Book/Book';
import TableHeader from './TableComponents/TableHeader';
import TableContent from './TableComponents/TableContent';

export default function BookTable() {

    const [books, setBooks] = React.useState<Book[]>([]);

    React.useEffect(() => {
        const api = async () => {
        const data = await fetch("https://localhost:5001/api/book", {
            method: "GET"
        });
        const jsonData = await data.json();
        console.log(jsonData);
        setBooks(jsonData);
        };


        api();
    }, []);

  return (
    <TableContent />
  );
}