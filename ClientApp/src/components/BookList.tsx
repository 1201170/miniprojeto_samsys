import React, { useCallback, useMemo, useState } from 'react';
import MaterialReactTable, {
  MaterialReactTableProps,
  MRT_Cell,
  MRT_ColumnDef,
  MRT_PaginationState,
  MRT_Row,
} from 'material-react-table';
import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  IconButton,
  MenuItem,
  Stack,
  TextField,
  Tooltip,
} from '@mui/material';
import { Delete, Edit } from '@mui/icons-material';
import { Book } from '../Book';
import {BookCreationModal} from './Modals/BookCreationModal';

export default function BookList () {
  const [createModalOpen, setCreateModalOpen] = useState(false);
  const [tableData, setTableData] = useState<Book[]>([]);
  const [validationErrors, setValidationErrors] = useState<{
    [cellId: string]: string;
  }>({});

  const [isError, setIsError] = useState(false);
  const [rowCount, setRowCount] = useState(0);
  const [isLoading, setIsLoading] = useState(false);
  const [isRefetching, setIsRefetching] = useState(false);
  const [actionFlick, setActionFlick] = useState(false);
  const [pagination, setPagination] = useState<MRT_PaginationState>({
    pageIndex: 0,
    pageSize: 5,
  });


  React.useEffect(() => {
      const api = async () => {

        if(!tableData.length){
          setIsLoading(true);
        } else {
          setIsRefetching(true);
        }

        try{
          const data = await fetch("https://localhost:5001/api/book/pageNumber="
                                    +(pagination.pageIndex+1)+"&pageSize="+pagination.pageSize, {
              method: "GET"
          });

          const jsonData = await data.json();
          const paginationHeader = await data.headers.get("x-pagination");
          const countRow = paginationHeader!==null ? JSON.parse(paginationHeader) : 0;
          console.log(jsonData);
          setTableData(jsonData);
          setRowCount(countRow.TotalCount);

        } catch (error) {
            setIsError(true);
            console.error(error);
            return;
        }
        setIsError(false);
        setIsLoading(false);
        setIsRefetching(false);  
      };
      api();
  }, [pagination.pageIndex,pagination.pageSize, actionFlick]);




  const handleCreateNewRow = (values: Book) => {

    updateAfterPost(values);

  };

  async function updateAfterPost(val: Book) : Promise<any> {

    const res = await postBook(val);

    setActionFlick(!actionFlick);

  }


  const handleSaveRowEdits: MaterialReactTableProps<Book>['onEditingRowSave'] =
    async ({ exitEditingMode, row, values }) => {
      if (!Object.keys(validationErrors).length) {
        tableData[row.index] = values;
        //send/receive api updates here, then refetch or update local table data for re-render
        //mandar o famoso put
        setActionFlick(!actionFlick);
        exitEditingMode(); //required to exit editing mode and close modal
      }
    };

  const handleCancelRowEdits = () => {
    setValidationErrors({});
  };

  const handleDeleteRow = useCallback(
    (row: MRT_Row<Book>) => {
      if (
        !window.confirm(`Are you sure you want to delete book with ISBN: ${row.getValue('bookIsbn')} ?`)
      ) {
        return;
      }
      //send api delete request here, then refetch or update local table data for re-render

      fetch("https://localhost:5001/api/book/"+row.getValue('bookIsbn')+"/softDelete", {
          method: "DELETE"
      });

      setActionFlick(!actionFlick);

    },
    [tableData],
  );

  const getCommonEditTextFieldProps = useCallback(
    (
      cell: MRT_Cell<Book>,
    ): MRT_ColumnDef<Book>['muiTableBodyCellEditTextFieldProps'] => {
      return {
        error: !!validationErrors[cell.id],
        helperText: validationErrors[cell.id],
        onBlur: (event) => {
          const isValid =
          cell.column.id === 'bookName'
            ? validateName(event.target.value)
            : cell.column.id === 'bookPrice'
            ? validatePrice(+event.target.value)
            : validateRequired(event.target.value);
          if (!isValid) {
            //set validation error for cell if invalid
            setValidationErrors({
              ...validationErrors,
              [cell.id]: `Error in ${cell.column.columnDef.header}`,
            });
          } else {
            //remove validation error for cell if valid
            delete validationErrors[cell.id];
            setValidationErrors({
              ...validationErrors,
            });
          }
        },
      };
    },
    [validationErrors],
  );

  const columns = useMemo<MRT_ColumnDef<Book>[]>(
    () => [
      {
        accessorKey: 'bookIsbn',
        header: 'ISBN',
        enableColumnOrdering: false,
        enableEditing: false, //disable editing on this column
        enableSorting: true,
        size: 80,
      },
      {
        accessorKey: 'bookName',
        header: 'Book Name',
        size: 140,
        muiTableBodyCellEditTextFieldProps: ({ cell }) => ({
          ...getCommonEditTextFieldProps(cell),
        }),
      },
      {
        accessorKey: 'bookAuthor',
        header: 'Book Author',
        enableEditing: false, //disable editing on this column
        size: 140,
        muiTableBodyCellEditTextFieldProps: ({ cell }) => ({
          ...getCommonEditTextFieldProps(cell),
        }),
      },
      {
        accessorKey: 'bookPrice',
        header: 'Price',
        muiTableBodyCellEditTextFieldProps: ({ cell }) => ({
          ...getCommonEditTextFieldProps(cell),
        }),
      },
    ],
    [getCommonEditTextFieldProps],
  );

  return (
    <>
      <MaterialReactTable
        displayColumnDefOptions={{
          'mrt-row-actions': {
            muiTableHeadCellProps: {
              align: 'center',
            },
            size: 120,
          },
        }}
        columns={columns}
        data={tableData}
        editingMode="modal" //default
        enableColumnOrdering
        enableEditing
        enablePagination={true}
        manualPagination
        onEditingRowSave={handleSaveRowEdits}
        onEditingRowCancel={handleCancelRowEdits}
        muiTablePaginationProps={{
          rowsPerPageOptions: [5, 10, 20],
        }}
        onPaginationChange={setPagination}
      
        muiToolbarAlertBannerProps={
          isError
            ? {
                color: 'error',
                children: 'Error loading data',
              }
            : undefined
        }
        renderRowActions={({ row, table }) => (
          <Box sx={{ display: 'flex', gap: '1rem' }}>
            <Tooltip arrow placement="left" title="Edit">
              <IconButton onClick={() => table.setEditingRow(row)}>
                <Edit />
              </IconButton>
            </Tooltip>
            <Tooltip arrow placement="right" title="Delete">
              <IconButton color="error" onClick={() => handleDeleteRow(row)}>
                <Delete />
              </IconButton>
            </Tooltip>
          </Box>
        )}
        renderTopToolbarCustomActions={() => (
          <Button
            color="secondary"
            onClick={() => setCreateModalOpen(true)}
            variant="contained"
          >
            Add New Book
          </Button>
        )}
        rowCount={rowCount}
        state={{
          isLoading,
          pagination,
          showAlertBanner: isError,
          showProgressBars: isRefetching,
        }}  
      />
      <BookCreationModal
        open={createModalOpen}
        onClose={() => setCreateModalOpen(false)}
        onSubmit={handleCreateNewRow}
      />
    </>
  );
};

const validateRequired = (value: string) => !!value.length;

const validateName = (bookName: string) =>
  !!bookName.length;

const validatePrice = (bookPrice: number) => bookPrice >= 0;


  function postBook(values: Book) : Promise<any> {

    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        "bookIsbn": values.bookIsbn,
        "bookAuthor": values.bookAuthor,
        "bookName": values.bookName,
        "bookPrice": parseFloat(values.bookPrice.toString()),
      }) // body data type must match "Content-Type" header
  };

    const data = fetch('https://localhost:5001/api/book', requestOptions);

    return data;

  }
