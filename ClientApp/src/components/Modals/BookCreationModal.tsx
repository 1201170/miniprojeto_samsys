import React, { useState, useEffect } from 'react'
import {BasicModal} from './BasicModal';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import { useForm } from 'react-hook-form';
import {yupResolver} from '@hookform/resolvers/yup';
import * as Yup from 'yup'
import { Book } from '../../Book';
import { Author } from '../../Author';
import { FormControl, InputLabel, MenuItem, Select } from '@mui/material';

const defaultInputValues = {
    bookIsbn: '',
    bookName: '',
    bookAuthor: "",
    bookPrice: 0
};

const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
  PaperProps: {
    style: {
      maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
      width: 250,
    },
  },
};


interface CreateModalProps {
    onClose: () => void;
    onSubmit: (values: Book) => void;
    open: boolean;
  }

export const BookCreationModal = ({open, onClose, onSubmit} : CreateModalProps) => {
    const [values, setValues] = useState(defaultInputValues);
    const [authorData, setAuthorData] = useState<Author[]>([]);


    const modalStyles = {
        inputFields: {
            display: 'flex',
            flexDirection: 'column',
            marginTop: '20px',
            marginBottom: '15px',
            '.MuiFormControl-root': {
                marginBottom: '20px',
            },
        },
    };

    const validationSchema : any = Yup.object().shape({
        bookIsbn: Yup.string()
            .required('Book ISBN is required'),
        bookName: Yup.string()
            .required('Book Name is required'),
        bookAuthor: Yup.string()
            .required('Author is required'),
        bookPrice: Yup.number()
            .required('Price is required')
            .positive('Price cannot be negative'),
    });

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm({
        resolver: yupResolver(validationSchema)
    });

    const handleChange = (value: any) => {
        setValues(value)
    };

    const addBook = (data:any) => {
        onSubmit(data);
        onClose();
    };

    
    useEffect(() => {
        if (open){ setValues(defaultInputValues)};

        const api = async () => {

            try{
              const data = await fetch("https://localhost:5001/api/author", {
                  method: "GET"
              });
    
              const jsonData = await data.json();
              console.log(jsonData);
              setAuthorData(jsonData);
    
            } catch (error) {
                console.error(error);
                return;
            }
          };
          api();    

    }, [open])

    const getContent = () => (
        <Box sx={modalStyles.inputFields}>
            <TextField
                placeholder="Book ISBN"
                label="Book ISBN"
                required
                {...register('bookIsbn')}
                error={errors.bookIsbn ? true : false}
                helperText={errors.bookIsbn?.message?.toString()}
                value={values.bookIsbn}
                onChange={(event) => handleChange({ ...values, bookIsbn: event.target.value })}
            />
            <TextField
                placeholder="Book Name"
                label="Book Name"
                required
                {...register('bookName')}
                error={errors.bookName ? true : false}
                helperText={errors.bookName?.message?.toString()}
                value={values.bookName}
                onChange={(event) => handleChange({ ...values, bookName: event.target.value })}
            />
            <FormControl >
                <InputLabel id="author-select-label">Book Author</InputLabel>
                <Select
                labelId="author-select-label"
                id="author-select"
                required
                {...register('bookAuthor')}
                error={errors.bookAuthor ? true : false}
                value={values.bookAuthor}
                onChange={(event) => handleChange({ ...values, bookAuthor: event.target.value })}
                autoWidth
                label="Book Author"
                MenuProps={MenuProps}
                >
                {authorData.map((author) => (
                    <MenuItem
                    key={author.authorId}
                    value={author.authorId}
                    >
                    {author.authorName}
                    </MenuItem>
                ))}
                </Select>
            </FormControl>
            <TextField
                placeholder="Book Price"
                label="Book Price"
                required
                {...register('bookPrice')}
                error={errors.bookPrice ? true : false}
                helperText={errors.bookPrice?.message?.toString()}
                value={values.bookPrice}
                onChange={(event) => handleChange({ ...values, bookPrice: event.target.value })}
            />
        </Box>
    );
    
    return (
        <BasicModal
            open={open}
            onClose={onClose}
            title="New Book"
            subTitle="Fill out inputs and hit 'submit' button."
            content={getContent()}
            onSubmit={handleSubmit(addBook)}
        />
            
    )
}