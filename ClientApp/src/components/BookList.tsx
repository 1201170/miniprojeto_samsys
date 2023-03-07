import React, { useCallback, useMemo, useState } from 'react';
import MaterialReactTable, {
  MaterialReactTableProps,
  MRT_Cell,
  MRT_ColumnDef,
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

export default function BookList () {
  const [createModalOpen, setCreateModalOpen] = useState(false);
  const [tableData, setTableData] = useState<Book[]>([]);
  const [validationErrors, setValidationErrors] = useState<{
    [cellIsbn: string]: string;
  }>({});

  React.useEffect(() => {
      const api = async () => {
      const data = await fetch("https://localhost:5001/api/book", {
          method: "GET"
      });
      const jsonData = await data.json();
      console.log(jsonData);
      setTableData(jsonData);
      };


      api();
  }, []);




  const handleCreateNewRow = (values: Book) => {

    updateAfterPost(values);

      /*
    // POST request using fetch inside useEffect React hook
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


    const data = fetch('https://localhost:5001/api/book', requestOptions).
    then((res) => {res.json()}).
    then((data) => {tableData.push(data), setTableData([...tableData])});
    //let newBook : Book = JSON.parse();
    //tableData.push(newBook);
    //setTableData([...tableData]);
    */

// empty dependency array means this effect will only run once (like componentDidMount in classes)
};

  async function updateAfterPost(val: Book) : Promise<any> {

    const res = await postBook(val);

    const resJson = await res.json();

    let newBook : Book = JSON.parse(JSON.stringify(resJson));
    tableData.push(newBook);
    setTableData([...tableData]);

  }


  const handleSaveRowEdits: MaterialReactTableProps<Book>['onEditingRowSave'] =
    async ({ exitEditingMode, row, values }) => {
      if (!Object.keys(validationErrors).length) {
        tableData[row.index] = values;
        //send/receive api updates here, then refetch or update local table data for re-render
        setTableData([...tableData]);
        exitEditingMode(); //required to exit editing mode and close modal
      }
    };

  const handleCancelRowEdits = () => {
    setValidationErrors({});
  };

  const handleDeleteRow = useCallback(
    (row: MRT_Row<Book>) => {
      if (
        !window.confirm(`Are you sure you want to delete ${row.getValue('bookIsbn')}`)
      ) {
        return;
      }
      //send api delete request here, then refetch or update local table data for re-render



      tableData.splice(row.index, 1);
      setTableData([...tableData]);
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
          const isValid = validateIsbn(event.target.value);
          if (!isValid) {
            //set validation error for cell if invalid
            setValidationErrors({
              ...validationErrors,
              [cell.id]: `${cell.column.columnDef.header} is required`,
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
      /*
      {
        accessorKey: 'state',
        header: 'State',
        muiTableBodyCellEditTextFieldProps: {
          select: true, //change to select for a dropdown
          children: states.map((state) => (
            <MenuItem key={state} value={state}>
              {state}
            </MenuItem>
          )),
        },
      },
      */
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
        onEditingRowSave={handleSaveRowEdits}
        onEditingRowCancel={handleCancelRowEdits}
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
      />
      <CreateNewBookModal
        columns={columns}
        open={createModalOpen}
        onClose={() => setCreateModalOpen(false)}
        onSubmit={handleCreateNewRow}
      />
    </>
  );
};

interface CreateModalProps {
  columns: MRT_ColumnDef<Book>[];
  onClose: () => void;
  onSubmit: (values: Book) => void;
  open: boolean;
}

//example of creating a mui dialog modal for creating new rows
export const CreateNewBookModal = ({
  open,
  columns,
  onClose,
  onSubmit,
}: CreateModalProps) => {
  const [values, setValues] = useState<any>(() =>
    columns.reduce((book, column) => {
      book[column.accessorKey ?? ''] = '';
      return book;
    }, {} as any),
  );

  const handleSubmit = () => {
    //put your validation logic here
    onSubmit(values);
    onClose();
  };

  return (
    <Dialog open={open}>
      <DialogTitle textAlign="center">Add New Book</DialogTitle>
      <DialogContent>
        <form onSubmit={(e) => e.preventDefault()}>
          <Stack
            sx={{
              width: '100%',
              minWidth: { xs: '300px', sm: '360px', md: '400px' },
              gap: '1.5rem',
            }}
          >
            {columns.map((column) => (
              <TextField
                key={column.accessorKey}
                label={column.header}
                name={column.accessorKey}
                onChange={(e) =>
                  setValues({ ...values, [e.target.name]: e.target.value })
                }
              />
            ))}
          </Stack>
        </form>
      </DialogContent>
      <DialogActions sx={{ p: '1.25rem' }}>
        <Button onClick={onClose}>Cancel</Button>
        <Button color="secondary" onClick={handleSubmit} variant="contained">
          Add New Book
        </Button>
      </DialogActions>
    </Dialog>
  );
};

const validateRequired = (value: string) => !!value.length;
const validateIsbn = (isbn: string) =>
  (isbn.length < 9);

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
