import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { Book } from '../Book';

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
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Book ISBN</TableCell>
            <TableCell align="right">Book Name</TableCell>
            <TableCell align="right">Book Author</TableCell>
            <TableCell align="right">Book Price</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {books.map((book) => (
            <TableRow
              key={book.bookIsbn}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                {book.bookIsbn}
              </TableCell>
              <TableCell align="right">{book.bookName}</TableCell>
              <TableCell align="right">{book.bookAuthor}</TableCell>
              <TableCell align="right">{book.bookPrice}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}