import * as React from 'react'

interface IImageProps {
    id?: string;
    src: string;
    alt?: string;
}

interface IImageState {
    placeholder: string;
}

class BaseImage extends React.Component<IImageProps, IImageState> {
    constructor(props: IImageProps) {
        super(props);
        this.state = { placeholder: '' }; // TODO: Setup BaseImage placeholder
    }

    render() {
        return (
            <img className="image" src={this.props.src} alt={this.props.alt} placeholder={this.state.placeholder} />
        );
    }
}

// IMAGE ==============================================================================
const containerClass = "image-container";

export class Image extends BaseImage {
    constructor(props: IImageProps) {
        super(props);
    }

    render() {
        const className = containerClass;

        return (
            <div id={this.props.id} className={className}>
                {super.render()}
            </div>
        );
    }
}

// CIRCLE IMAGE ==============================================================================
export class CircleImage extends BaseImage {
    constructor(props: IImageProps) {
        super(props);
    }

    render() {
        const className = containerClass + ' img-circle';

        return (
            <div id={this.props.id} className={className}>
                {super.render()}
            </div>
        );
    }
}