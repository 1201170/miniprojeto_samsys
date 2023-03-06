import { TableCell, TableHead, TableRow, TableSortLabel } from "@mui/material";


export default function TableHeader(props: any){

    const {orderDirection, valueToOrderBy, handleRequestSort} = props;

    const createSortHandler = (property:any) => (event:any) => {
        handleRequestSort(event, property)
    }

    return (
            <TableHead>
                <TableRow>

                    <TableCell>
                        <TableSortLabel 
                            key='bookIsbn'
                            active={valueToOrderBy === 'bookIsbn'} 
                            direction={valueToOrderBy === 'bookIsbn' ? orderDirection : 'asc'}
                            onClick={createSortHandler('bookIsbn')}
                            >
                            Book Isbn
                        </TableSortLabel>
                    </TableCell>

                    <TableCell>
                        <TableSortLabel
                            key='bookName'
                            active={valueToOrderBy === 'bookName'} 
                            direction={valueToOrderBy === 'bookName' ? orderDirection : 'asc'}
                            onClick={createSortHandler('bookName')}

                            >
                            Book Name
                        </TableSortLabel>
                    </TableCell>


                    <TableCell>
                        <TableSortLabel
                         key='bookAuthor'
                         active={valueToOrderBy === 'bookAuthor'} 
                         direction={valueToOrderBy === 'bookAuthor' ? orderDirection : 'asc'}
                         onClick={createSortHandler('bookAuthor')}

                         >
                            Book Author
                        </TableSortLabel>
                    </TableCell>


                    <TableCell>
                        <TableSortLabel
                            key='bookPrice'
                            active={valueToOrderBy === 'bookPrice'} 
                            direction={valueToOrderBy === 'bookPrice' ? orderDirection : 'asc'}
                            onClick={createSortHandler('bookPrice')}

                            >
                            Book Price
                        </TableSortLabel>
                    </TableCell>
                </TableRow>
            </TableHead>
    )

}