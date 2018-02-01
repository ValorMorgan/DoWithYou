import * as React from 'react'

interface IImageProps {
    src: string;
    alt: string;
    classes: string;
}

interface IImageState {
    placeholder: string;
}

class BaseImage extends React.Component<IImageProps, IImageState> {
    constructor(props: IImageProps) {
        super(props);
        this.state = { placeholder: '' };
    }

    render() {
        return (
            <img className="image" src={this.props.src} alt={this.props.alt} placeholder={this.state.placeholder} />
        );
    }
}

export class Image extends BaseImage {
    constructor(props: IImageProps) {
        super(props);
        props.classes = "image-container " + props.classes;
    }

    render() {
        return (
            <div className={this.props.classes}>
                {super.render()}
            </div>
        );
    }
}

export class CircleImage extends BaseImage {
    constructor(props: IImageProps) {
        super(props);
        props.classes = "image-container img-circle " + props.classes;
    }

    render() {
        return (
            <div className={this.props.classes}>
                {super.render()}
            </div>
        );
    }
}