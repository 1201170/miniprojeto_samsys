import { Table, TableCell, TableContainer, TableFooter, TableHead, TablePagination, TableRow, TableSortLabel } from "@mui/material";
import React from "react";
import Book from "../../models/Book/Book";
import TableHeader from "./TableHeader";


function getComparator(order:any, orderBy:any){
    return order === 'desc' ? 
    (a:any,b:any) => descendingComparator(a,b,orderBy) :
    (a:any,b:any) => -descendingComparator(a,b,orderBy);
}

function descendingComparator(a:any,b:any,orderBy:any){

    if(b[orderBy] < a[orderBy]){
        return -1;
    }

    if(b[orderBy] > a[orderBy]){
        return 1;
    }

    return 0;
}

const sortedRowInformation = (rowArray:any, comparator:any) => {
    const stabilizedRowArray : any = rowArray.map((el:any,index:any) => [el, index]);
    stabilizedRowArray.sort((a:any ,b:any) =>
        {const order = comparator(a[0],b[0]);
            if(order!==0){
                return order;
            } else {
                return a[1]-b[1];
            }
        }
    )
    return stabilizedRowArray.map((el:any) => el[0]);
}       


export default function TableContent(){

    const [book, setBooks] = React.useState<Book[]>([]);

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



    const [orderDirection, setOrderDirection] = React.useState('asc')
    const [valueToOrderBy, setValueToOrderBy] = React.useState('bookIsbn')
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(1);

    const handleRequestSort = (event:any, property:any) => {
        const isAscending = (valueToOrderBy === property && orderDirection === 'asc');
        console.log(property);
        setValueToOrderBy(property);
        setOrderDirection(isAscending ? 'desc' : 'asc');
    }

        
    const handleChangePage = (event:any, newPage:any) => {
        setPage(newPage);
    }

    const handleChangesRowsPerPage = (event:any) => {
        setRowsPerPage(parseInt(event.target.value));
        setPage(0);
    }

    return (
        <div>
            <TableContainer>
                <Table>
                    <TableHeader 
                        orderDirection={orderDirection}
                        valueToOrderBy={valueToOrderBy}
                        handleRequestSort={handleRequestSort}
                    />
                    {
                        sortedRowInformation(
                            book, getComparator(orderDirection, valueToOrderBy))
                            .slice(page*rowsPerPage,page*rowsPerPage+rowsPerPage)
                            .map((b:any, index:any) => (
                                <TableRow key={index}>
                                    <TableCell>
                                        {b.bookIsbn}
                                    </TableCell>
                                    <TableCell>
                                        {b.bookName}
                                    </TableCell>
                                    <TableCell>
                                        {b.bookAuthor}
                                    </TableCell>
                                    <TableCell>
                                        {b.bookPrice} €
                                    </TableCell>
                                </TableRow>
                            )
                        )
                    }
                </Table>
            </TableContainer>
                <TablePagination 
                rowsPerPageOptions={[1,2,3]} 
                count={book.length}
                rowsPerPage={rowsPerPage}
                page={page}
                onPageChange={handleChangePage}
                onRowsPerPageChange={handleChangesRowsPerPage}
                />
            </div>
    )
}