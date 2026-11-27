import { useEffect, useState } from "react";
import styles from "./global.components.module.css";
import { MODULES } from "../../../Config/Modules";
import { useLocation, useNavigate } from "react-router-dom";
import { FaTimes } from "react-icons/fa";

const WHEEL_SIZE = 400;
const CENTER = WHEEL_SIZE / 2;
const R_OUTER = 212;
const R_INNER = 96;
const GAP_PX = 8;

function polarToCartesian(cx: number, cy: number, radius: number, angleDeg: number) {
    const rad = (angleDeg * Math.PI) / 180;
    return {
        x: cx + radius * Math.cos(rad),
        y: cy + radius * Math.sin(rad),
    };
}

function createSectorPath(startDeg: number, endDeg: number): string {
    const halfGap = GAP_PX / 2;
    const outerInset = (Math.asin(halfGap / R_OUTER) * 180) / Math.PI;
    const innerInset = (Math.asin(halfGap / R_INNER) * 180) / Math.PI;

    const oStart = startDeg + outerInset;
    const oEnd = endDeg - outerInset;
    const iStart = startDeg + innerInset;
    const iEnd = endDeg - innerInset;

    const outerStartPt = polarToCartesian(CENTER, CENTER, R_OUTER, oStart);
    const outerEndPt = polarToCartesian(CENTER, CENTER, R_OUTER, oEnd);
    const innerEndPt = polarToCartesian(CENTER, CENTER, R_INNER, iEnd);
    const innerStartPt = polarToCartesian(CENTER, CENTER, R_INNER, iStart);

    const largeArc = oEnd - oStart > 180 ? 1 : 0;

    return [
        `M ${outerStartPt.x.toFixed(2)} ${outerStartPt.y.toFixed(2)}`,
        `A ${R_OUTER} ${R_OUTER} 0 ${largeArc} 1 ${outerEndPt.x.toFixed(2)} ${outerEndPt.y.toFixed(2)}`,
        `L ${innerEndPt.x.toFixed(2)} ${innerEndPt.y.toFixed(2)}`,
        `A ${R_INNER} ${R_INNER} 0 ${largeArc} 0 ${innerStartPt.x.toFixed(2)} ${innerStartPt.y.toFixed(2)}`,
        "Z",
    ].join(" ");
}

export const Menu = () => {
    const [isOpen, setOpen] = useState<boolean>(false);
    const location = useLocation();
    const navigate = useNavigate();

    useEffect(() => {
        function handleKeyDown(e: KeyboardEvent) {
            const target = e.target as HTMLElement | null;
            const isTyping =
                target?.tagName === "INPUT" ||
                target?.tagName === "TEXTAREA" ||
                target?.tagName === "SELECT" ||
                target?.isContentEditable;

            if (isTyping) return;

            if (e.code === "KeyE") {
                setOpen((prev) => !prev);
            } else if (e.code === "Escape") {
                setOpen(false);
            }
        }

        window.addEventListener("keydown", handleKeyDown);
        return () => window.removeEventListener("keydown", handleKeyDown);
    }, []);

    const visibleModules = MODULES.filter((m) => !m.hidden);
    const sliceAngle = 360 / visibleModules.length;
    const midRadius = (R_OUTER + R_INNER) / 2;

    function handleSelect(url: string) {
        setOpen(false);
        navigate(url);
    }

    return (
        <div
            className={`${styles.menuOverlay} ${isOpen ? styles.menuOpen : ""}`}
            onClick={() => setOpen(false)}
            aria-hidden={!isOpen}
        >
            <nav
                className={styles.menuWheel}
                onClick={(e) => e.stopPropagation()}
            >
                <svg
                    className={styles.menuSvg}
                    viewBox={`0 0 ${WHEEL_SIZE} ${WHEEL_SIZE}`}
                    width={WHEEL_SIZE}
                    height={WHEEL_SIZE}
                >
                    {visibleModules.map((module, index) => {
                        const startDeg = -90 + index * sliceAngle;
                        const endDeg = startDeg + sliceAngle;
                        const midDeg = (startDeg + endDeg) / 2;
                        const labelPos = polarToCartesian(CENTER, CENTER, midRadius, midDeg);
                        const isActive = location.pathname.startsWith(module.url);
                        const pathData = createSectorPath(startDeg, endDeg);

                        return (
                            <g
                                key={module.name}
                                className={`${styles.menuSector} ${isActive ? styles.menuSectorActive : ""}`}
                                style={{ "--delay": `${index * 35}ms` } as React.CSSProperties}
                                role="link"
                                tabIndex={isOpen ? 0 : -1}
                                onClick={() => handleSelect(module.url)}
                                onKeyDown={(e) => {
                                    if (e.key === "Enter" || e.key === " ") {
                                        e.preventDefault();
                                        handleSelect(module.url);
                                    }
                                }}
                            >
                                <path d={pathData} className={styles.menuSectorPath} />
                                <foreignObject
                                    x={labelPos.x - 45}
                                    y={labelPos.y - 28}
                                    width={90}
                                    height={56}
                                    className={styles.menuSectorContent}
                                >
                                    <div className={styles.menuSectorInner}>
                                        <module.icon className={styles.menuIcon} />
                                        <span className={styles.menuLabel}>{module.name}</span>
                                    </div>
                                </foreignObject>
                            </g>
                        );
                    })}
                </svg>

                <button type="button" className={styles.menuCenter} onClick={() => setOpen(false)}>
                    <FaTimes />
                    <span>E / ESC</span>
                </button>
            </nav>
        </div>
    );
};
