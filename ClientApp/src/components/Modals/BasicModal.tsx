import React from 'react'
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Modal from '@mui/material/Modal';
import CommonButton from '../Buttons/CommonButton';
import { modalStyles } from './styles';
import Book from '../../models/Book/Book';

interface CreateModalProps {
    onClose: () => void;
    onSubmit:any;
    open: boolean;
    title: string;
    subTitle: string;
    content: any;
}

export const BasicModal = ({open, onClose, title, subTitle, content, onSubmit} : CreateModalProps) => {

    return (
        <Modal open={open} onClose={onClose} >
            <Box sx={modalStyles.wrapper}>
                <Typography
                    variant="h6"
                    component="h2"
                >
                    {title}
                </Typography>
                <Typography sx={{ mt: 2 }}>
                    {subTitle}
                </Typography>
                {content}
                <Box sx={modalStyles.buttons}>
                    <CommonButton
                        variant="contained"
                        onClick={onSubmit}
                    >
                        Submit
                    </CommonButton>
                    <CommonButton onClick={onClose}>Cancel</CommonButton>
                </Box>
            </Box>
        </Modal>
    )
}