import * as React from 'react';
import { ICommonProps } from './Misc';

interface IImageProps extends ICommonProps {
    src: string;
    alt?: string;
}

const PlaceholderImage = () =>
    <img className="image" alt="placeholder"/>;

export const Image = (props: IImageProps) => props ?
    <img className={`image ${props.className ? props.className : ''}`.trim()} /> :
    <PlaceholderImage />;

export const CircleImage = (props: IImageProps) => props ?
    <Image {...props} className={`image--circle ${props.className ? props.className : ''}`.trim()} /> :
    <PlaceholderImage />;