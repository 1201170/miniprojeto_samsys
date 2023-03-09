import React, { useState, useEffect } from 'react'
import {BasicModal} from './BasicModal';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import { useForm } from 'react-hook-form';
import {yupResolver} from '@hookform/resolvers/yup';
import * as Yup from 'yup'
import { Book } from '../../Book';

const defaultInputValues = {
    bookIsbn: '',
    bookName: '',
    bookAuthor: '',
    bookPrice: 0
};

interface CreateModalProps {
    onClose: () => void;
    onSubmit: (values: Book) => void;
    open: boolean;
  }

export const BookCreationModal = ({open, onClose, onSubmit} : CreateModalProps) => {
    const [values, setValues] = useState(defaultInputValues);

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

    //const isbnRegExp =;

    const validationSchema : any = Yup.object().shape({
        bookIsbn: Yup.string()
            .required('Book ISBN is required'),
        bookName: Yup.string()
            .required('Book Name is required'),
        bookAuthor: Yup.string()
            .required('Author ID is required'),
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

    /*
    const handleSubmit = () => {
        let book : Book = new Book(values.bookIsbn, values.bookAuthor, values.bookName, values.bookPrice);
        onSubmit(book);
        onClose();
      };
      */

    const addBook = (data:any) => {
        onSubmit(data);
        onClose();
    };

    

    useEffect(() => {
        if (open) setValues(defaultInputValues);
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
            <TextField
                placeholder="Book Author"
                label="Book Author"
                required
                {...register('bookAuthor')}
                error={errors.bookAuthor ? true : false}
                helperText={errors.bookAuthor?.message?.toString()}
                value={values.bookAuthor}
                onChange={(event) => handleChange({ ...values, bookAuthor: event.target.value })}
            />
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