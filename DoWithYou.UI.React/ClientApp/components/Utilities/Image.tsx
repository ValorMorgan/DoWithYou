import * as React from 'react';
import { ICommonProps } from './Misc';

// TODO: Make "alt" text appear as a popup next to image on hover

interface IImageProps extends ICommonProps {
    src: string;
    alt?: string;
}

const PlaceholderImage = () =>
    <img className="image" alt="placeholder"/>;

export const Image = (props: IImageProps) => {
    const {className, ...other} = props;

    return (
        <img {...other} className={`image ${className ? className : ''}`.trim()} />
    );
}

export const CircleImage = (props: IImageProps) => {
    const {className, ...other} = props;

    return (
        <img {...other} className={`image--circle ${className ? className : ''}`.trim()} />
    );
}