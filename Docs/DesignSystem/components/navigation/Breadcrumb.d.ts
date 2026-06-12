export interface CrumbItem {
  label: React.ReactNode;
  href?: string;
  onClick?: (e: React.MouseEvent) => void;
}

export interface BreadcrumbProps {
  /** Trail from root → current. The last item renders as the current page. */
  items: CrumbItem[];
}

/** Location trail that reinforces "you can get anywhere from here". */
export function Breadcrumb(props: BreadcrumbProps): JSX.Element;
