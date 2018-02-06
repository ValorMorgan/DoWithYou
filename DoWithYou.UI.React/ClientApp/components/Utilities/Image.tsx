import * as React from 'react';
import { ICommonProps } from './Misc';

interface IImageProps extends ICommonProps {
    src: string;
    alt?: string;
}

// TODO: Image placeholder
const imageClass = 'image';
const placeHolder = '';

const PlaceholderImage = () =>
    <img className={imageClass} placeholder={placeHolder} />;

export const Image = (props: IImageProps) => props ?
    <img {...props} className={`${imageClass} ${props.className ? props.className : ''}`.trim()} placeholder={placeHolder} /> :
    <PlaceholderImage />;

export const CircleImage = (props: IImageProps) => props ?
    <Image {...props} className={`img-circle ${props.className ? props.className : ''}`.trim()} /> :
    <PlaceholderImage />;