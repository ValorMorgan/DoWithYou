import * as React from 'react';
import { ICommonProps } from './Utilities';

interface IImageProps extends ICommonProps {
    src: string;
    alt?: string;
}

// TODO: Image placeholder
const placeHolder = '';

export const Image = (props: IImageProps) =>
    <img {...props} className={`image ${props.className}`.trim()} placeholder={placeHolder} />;

export const CircleImage = (props: IImageProps) =>
    <img {...props} className={`image img-circle ${props.className}`.trim()} placeholder={placeHolder} />;